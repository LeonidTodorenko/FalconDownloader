using FalconDownloader.Contracts;
using FalconDownloader.Models;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FalconDownloader.Services
{
    public class EmailExchangeService : IEmailService
    {
        /// <summary>
        /// Create an instance of EmailExchangeService
        /// </summary>
        /// <param name="login">login(email)</param>
        /// <param name="password">password</param>
        /// <param name="subjectSubstring">a part of email subject for searching</param>
        public EmailExchangeService(EmailSeviceData data)
        {
            _exchange = new ExchangeService();
            _login = data.Login;
            _exchange.Credentials = new WebCredentials(_login, data.Password);
            _exchange.UseDefaultCredentials = false;
            _exchange.TraceEnabled = true;
            _subjectSubstring = data.SubjectSubstring;
            _projectsEmail = data.ProjectsEmail;
            _adminEmail = data.AdminEmail;
            _bodyPrefix = data.ForwardMessageAddition;
        }

        public IResult<IEnumerable<EmailMessage>> FindUnreadEmails()
        {
            try
            {
                if (_exchange.Url == null)
                {
                    _exchange.AutodiscoverUrl(_login, RedirectionUrlValidationCallback);
                } 

                // Add a search filter that searches on the body or subject.
                var searchFilterCollection = new List<SearchFilter>();
                searchFilterCollection.Add(new SearchFilter.ContainsSubstring(ItemSchema.Subject, _subjectSubstring));
                searchFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));

                // Create the search filter.
                SearchFilter searchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());

                // Create a view with a page size of 50.
                // Identify the Subject and DateTimeReceived properties to return. Indicate that the base property will be the item identifier
                // Order the search results by the DateTimeReceived in descending order.
                // Set the traversal to shallow. (Shallow is the default option; other options are Associated and SoftDeleted.)
                ItemView view = new ItemView(50);
                view.PropertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Subject, ItemSchema.DateTimeReceived);
                view.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
                view.Traversal = ItemTraversal.Shallow;

                // Send the request to search the Inbox and get the results.
                FindItemsResults<Item> findResults = _exchange.FindItems(WellKnownFolderName.Inbox, searchFilter, view);
                var messages = findResults.Items.OfType<EmailMessage>();

                return new Result<IEnumerable<EmailMessage>>(messages);
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<EmailMessage>>(null, ex.Message);
            }
        }

        public IResult<IEnumerable<EmailMessage>> DownloadEmails(IEnumerable<EmailMessage> messages, CancellationToken token)
        {
            try
            {
                var emails = new List<EmailMessage>();
                foreach (var message in messages)
                {
                    if (token.IsCancellationRequested)
                        break;
                    PropertySet psPropset = new PropertySet();
                    psPropset.RequestedBodyType = BodyType.Text;
                    psPropset.BasePropertySet = BasePropertySet.FirstClassProperties;
                    emails.Add(EmailMessage.Bind(_exchange, message.Id, psPropset));
                }

                return new Result<IEnumerable<EmailMessage>>(emails);
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<EmailMessage>>(null, ex.Message);
            }
        }

        public IResult<bool> MarkAsRead(EmailMessage msg)
        {
            try
            {
                msg.IsRead = true;
                msg.Update(ConflictResolutionMode.AlwaysOverwrite);
                return new Result<bool>(true);
            }
            catch(Exception ex)
            {
                return new Result<bool>(false, ex.Message);
            }
        }

        public IResult<bool> ForwardEmail(EmailMessage msg)
        {
            try
            {
                msg.Forward(_bodyPrefix, msg.ToRecipients.First(), new EmailAddress(_projectsEmail));
                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, ex.Message);
            }
        }

        public IResult<bool> ForwardEmailToAdmin(EmailMessage msg, string error)
        {
            try
            {
                var prefix = string.Format("Attachment from this message could not be downloaded. Error: {0}", error);
                msg.Forward(prefix, _adminEmail.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries).Select(a => new EmailAddress(a)));
                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, ex.Message);
            }
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            /*
            // The default for the validation callback is to reject the URL.
            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            return new Uri(redirectionUrl).Scheme == "https";
            */

            // Return true if the URL is an HTTPS URL.
            return redirectionUrl.ToLower().StartsWith("https://");
        }

        private readonly string _login;
        private readonly ExchangeService _exchange;
        private readonly string _subjectSubstring;
        private readonly string _projectsEmail;
        private readonly string _adminEmail;
        private readonly string _bodyPrefix;
    }
}
