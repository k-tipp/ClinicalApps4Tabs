namespace SmaNa.MidataAccess
{
    /// <summary>
    /// Basically copied from https://github.com/i4mi/midata.js/blob/master/src/api.ts and adopted to c#
    /// All Classes needed for MiData-Communication 
    /// </summary>

    public class AuthRequest
    {
        public string appname; // internal name of the application
        public string secret;   // the secret key that has been chosen on the development portal
        public string username; // the email of the user
        public string password; // the user's password
        public string role;  // the role of the user (optional, default: "member")
    }

    /**
     * The authentication request payload for a new authentication.
     */
    public class RefreshAutRequest
    {
        public string appname;      // internal name of the application
        public string secret;       // the secret key that has been chosen on the development portal
        public string refreshToken; // the refresh token obtained from a previous login
    }

    /**
     * The user role in an authentication request.
     */
    public enum UserRole { member, provider, developer, research }
    //'member'    |  // members of the cooperative (default)
    //'provider'  |  // healthcare providers
    //'developer' |  // developers
    //'research'  ;  // researchers


    /**
     * A response to successful authentication request.
     */
    public class AuthResponse
    {
        public string authToken;
        public string refreshToken;
        public string status;
        public string owner;
    }

    /**
     * A request to create a new record.
     */
    public class CreateRecordRequest
    {
        public string authToken;        // the token from the authentication request,
        public string name;             // a name for the record,
        public string description;      // a description for the record,
        public string format;           // a string describing the technical data format,
        public string code;  // a string or an array of strings describing
                             // the data content (each string contains system and code),
        public string owner;           // the id of the owner of the record. (optional, default is "self")
        public string data;             // JSON string containing the record itself.
    }

    /**
     * The response to a successful create record request.
     */
    public class CreateRecordResponse
    {
        public string _id;     // id of the new record
        public long created; // the creation timestamp of the new record
                             //(in milliseconds after 01.Jan.1970 UTC)
    }

    public class FHIRMessage
    {
        public BodyWeight Content;
    }
}