using Newtonsoft.Json.Linq;
using Notary2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notary2015.Controllers
{
    public class WebController : Controller
    {
        //
        // GET: /Web/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Second()
        {
            return View();
        }


        public bool IsAuth()
        {
            string token = (string)Session["access_token"];
            return token != null && token != String.Empty;
        }

        public ActionResult vk(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                OAuth2 vk = new OAuth2("4907614", "DETrSX49YudoR4vK7CEd", "http://api.vkontakte.ru/oauth/authorize", "https://api.vkontakte.ru/oauth/access_token", "http://localhost:45227/Web/vk");
                vk.Code = code;
                OAuth2Token token = vk.GetAccessToken(new Dictionary<string, string> { { "client_secret", "DETrSX49YudoR4vK7CEd" } }, OAuth2.AccessTokenType.JsonDictionary);
                if (token != null)
                {
                    if (token.dictionary_token != null)
                    {
                        Session.Add("access_token", token.dictionary_token["access_token"]);
                        Session.Add("user_id", token.dictionary_token["user_id"]);
                        string res = OAuth2UserData.GetVKUserData(token.dictionary_token["access_token"], new Dictionary<string, string>() { 
                                        {"user_ids", token.dictionary_token["user_id"]},
                                        {"fields", "photo_200, screen_name"}});
                        res = res.Replace("\"", "'");
                        res = res.Replace("{'response':[", "");
                        res = res.Replace("}]}", "}");
                        dynamic stuff = JObject.Parse(res);
                        string first_name = stuff.first_name;
                        Session.Add("first_name", first_name);
                        string last_name = stuff.last_name;
                        Session.Add("last_name", last_name);
                        string screen_name = stuff.screen_name;
                        Session.Add("screen_name", screen_name);
                        string photo_id = stuff.photo_200;
                        Session.Add("photo_id", photo_id);
                    }
                }
            }
            else
            {
                OAuth2 vk = new OAuth2("4907614", "DETrSX49YudoR4vK7CEd", "http://api.vkontakte.ru/oauth/authorize", "https://api.vkontakte.ru/oauth/access_token", "http://localhost:45227/Web/vk");
                vk.GetAuthCode(new Dictionary<string, string>() { { "display", "popup" } });
            }
            return RedirectToAction("");
        }
    }
}
