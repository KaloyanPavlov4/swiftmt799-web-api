namespace swiftmt799_web_api.Models
{
    public class SwiftMT799Message
    {
        private string _basicHeader;
        private string _applicationHeader;
        private string _userHeader;
        private string _transactionReference;
        private string _relatedReference;
        private string _narrative;
        private string _trailer;

        public SwiftMT799Message(string basicHeader, string applicationHeader,
            string? userHeader,
            string transactionReference,
            string? relatedReference,
            string narrative,
            string? trailer)
        {
            BasicHeader = basicHeader;
            ApplicationHeader = applicationHeader;
            UserHeader = userHeader;
            TransactionReference = transactionReference;
            RelatedReference = relatedReference;
            Narrative = narrative;
            Trailer = trailer;
        }

        public string BasicHeader
        {
            get => _basicHeader;
            set
            {
                if (value == null) throw new ArgumentNullException("Missing block 1");
                _basicHeader = value;
            }
        }

        public string ApplicationHeader
        {
            get => _applicationHeader;
            set
            {
                if (value == null) throw new ArgumentNullException("Missing block 2");
                _applicationHeader = value;
            }
        }

        public string? UserHeader
        {
            get => _userHeader;
            set => _userHeader = value;
        }

        public string TransactionReference
        {
            get => _transactionReference;
            set
            {
                if (value == null) throw new ArgumentNullException("Missing field 20");
                if (value.Length > 16) throw new ArgumentException("Field 20 must not be more than 16 characters long");
                _transactionReference = value;
            }
        }

        public string? RelatedReference
        {
            get => _relatedReference;
            set
            {
                if (value.Length > 16) throw new ArgumentException("Field 21 must not be more than 16 characters long");
                _relatedReference = value;
            }
        }

        public string Narrative
        {
            get => _narrative;
            set
            {
                if (value.Length > 7000) throw new ArgumentException("Field 79 must not be more than 7000 characters long");
                _narrative = value;
            }
        }

        public string? Trailer
        {
            get => _trailer;
            set => _trailer = value;
        }
    }
}
