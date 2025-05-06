using Facebook;

namespace FacebookApiIUse.Services;

public class FacebookService
{
    private readonly string _appId;
    private readonly string _appSecret;

    public FacebookService(IConfiguration configuration)
    {
        _appId = configuration["Facebook:AppId"];
        _appSecret = configuration["Facebook:AppSecret"];
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
