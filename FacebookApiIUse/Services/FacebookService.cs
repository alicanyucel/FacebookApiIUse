using Facebook;

namespace FacebookApiIUse.Services
{
    public class FacebookService
    {
        private readonly string _appId;
        private readonly string _appSecret;

        public FacebookService(IConfiguration configuration)
        {
            _appId = configuration["Facebook:AppId"];
            _appSecret = configuration["Facebook:AppSecret"];
        }
        public async Task<dynamic> UploadDocumentFileAsync(string pageAccessToken, Stream fileStream, string fileName, string caption = "")
        {
            var fb = new FacebookClient(pageAccessToken);
            byte[] fileData;
            using (var ms = new MemoryStream())
            {
                await fileStream.CopyToAsync(ms);
                fileData = ms.ToArray();
            }
            var contentType = GetContentType(fileName);

            var mediaObject = new FacebookMediaObject
            {
                ContentType = contentType,
                FileName = fileName
            }.SetValue(fileData);

            var parameters = new Dictionary<string, object>
            {
                { "caption", caption },
                { "source", mediaObject }
            };

            return await fb.PostTaskAsync("/me/photos", parameters);
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".txt" => "text/plain",
                _ => "application/octet-stream",
            };
        }

        public async Task<dynamic> GetFacebookUserProfileAsync(string accessToken)
        {
            var fb = new FacebookClient(accessToken);
            return await fb.GetTaskAsync("me?fields=id,name,email,picture.width(200).height(200)");
        }

        public async Task<dynamic> UploadPhotoAsync(string pageAccessToken, string imageUrl, string caption = "")
        {
            var fb = new FacebookClient(pageAccessToken);

            var parameters = new Dictionary<string, object>
            {
                { "url", imageUrl },
                { "caption", caption }
            };

            return await fb.PostTaskAsync("/me/photos", parameters);
        }
    }
}
