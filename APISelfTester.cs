using Logic;
using System;
using Data;
using GraphQLParser;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;


namespace Test
{
    public class APISelfTester
    {
        private const string CanvasApiEndpointUser = "https://fhict.instructure.com/api/v1/users/self";
        public string accessToken = "2464~jTObAR4QZulMSgJ9GSVMwhnufYYmcpJ2qzZ55TQQ5fWfpsQgV6QX0j1pAjiIHSxU";

        [Fact]
        public async Task TestUserAPI()
        {
            //Arrange
            var userclient = new HttpClient();
            userclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            bool statuscode = true;

            //Act
            var response = await userclient.GetAsync(CanvasApiEndpointUser);

            //Assert
            Assert.Equal(statuscode, response.IsSuccessStatusCode);
        }
        //[Fact]
        //public async Task TestUserAPIWithWrongToken()
        //{
        //    //Arrange
        //    var userclient = new HttpClient();
        //    String token = "BadToken";
        //    userclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    bool statuscode = true;

        //    //Act
        //    var response = await userclient.GetAsync(CanvasApiEndpointUser);

        //    //Assert
        //    Assert.NotEqual(statuscode, response.IsSuccessStatusCode);
        //}

        [Fact]
        public async Task TestResponseToJSON()
        {
            //Arrange
            var userclient = new HttpClient();
            userclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            String responseJSON = "{\"id\":22810,\"name\":\"Schultz, Sebastiaan Schultz S.W.\",\"created_at\":\"2020-10-27T00:04:58+01:00\",\"sortable_name\":\"Schultz, Sebastiaan Schultz S.W.\",\"short_name\":\"Schultz, Sebastiaan Schultz S.W.\",\"avatar_url\":\"https://fhict.instructure.com/images/messages/avatar-50.png\",\"last_name\":\"Schultz\",\"first_name\":\"Sebastiaan Schultz S.W.\",\"locale\":null,\"effective_locale\":\"en\",\"permissions\":{\"can_update_name\":false,\"can_update_avatar\":true,\"limit_parent_app_web_access\":false}}";


            //Act
            String responseFromAPI = await userclient.GetStringAsync(CanvasApiEndpointUser);

            //Assert
            Assert.Equal(responseFromAPI, responseJSON);
        }


        [Fact]
        public async Task TestJSONSerialize()
        {
            //Arrange
            String responseJSON = "{\"id\":22810,\"name\":\"Schultz, Sebastiaan Schultz S.W.\",\"created_at\":\"2020-10-27T00:04:58+01:00\",\"sortable_name\":\"Schultz, Sebastiaan Schultz S.W.\",\"short_name\":\"Schultz, Sebastiaan Schultz S.W.\",\"avatar_url\":\"https://fhict.instructure.com/images/messages/avatar-50.png\",\"last_name\":\"Schultz\",\"first_name\":\"Sebastiaan Schultz S.W.\",\"locale\":null,\"effective_locale\":\"en\",\"permissions\":{\"can_update_name\":false,\"can_update_avatar\":true,\"limit_parent_app_web_access\":false}}";
            User mock = new User();
            mock.id = 22810;

            //Act
            User actual = JsonSerializer.Deserialize<User>(responseJSON);

            //Assert
            Assert.Equal(actual.id , mock.id);
        }

        
    }  
}
