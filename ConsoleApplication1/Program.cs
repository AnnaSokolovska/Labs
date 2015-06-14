using Neo4jClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;

namespace ConsoleApplication1
{
    class Program
    {
        static Collection<Notary> addNotary()
        {
            var request = WebRequest.Create("http://localhost:45227/api/notary");
            request.ContentType = "application/json; charset=utf-8";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            text = text.Replace("\"", "'");
            text = text.Replace("[", "{notaries:[");
            text = text.Replace("]", "]}");
            dynamic stuff = JObject.Parse(text);
            JArray items = (JArray)stuff["notaries"];
            int length = items.Count;
            Collection<Notary> notaries = new Collection<Notary>();
            for (int i = 0; i < length; i++)
            {
                Notary notary = new Notary();
                notary.idNotary = stuff.notaries[i].idNotary;
                notary.Surname = stuff.notaries[i].Surname;
                notary.Name = stuff.notaries[i].Name;
                notary.MiddleName = stuff.notaries[i].MiddleName;
                notary.Certificate = stuff.notaries[i].Certificate;
                if (stuff.notaries[i].Suspension != null)
                    notary.Suspension = stuff.notaries[i].Suspension;
                notary.Phone = stuff.notaries[i].Phone;
                notary.Address = stuff.notaries[i].Address;
                if (stuff.notaries[i].Office_idOffice != null)
                    notary.Office_idOffice = stuff.notaries[i].Office_idOffice;
                notary.District_idDistrict = stuff.notaries[i].District_idDistrict;
                notaries.Add(notary);
            }
            return notaries;
        }

        static Collection<District> addDistrict()
        {
            var request = WebRequest.Create("http://localhost:45227/api/District");
            request.ContentType = "application/json; charset=utf-8";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            text = text.Replace("\"", "'");
            text = text.Replace("'я", "я");
            text = text.Replace("[", "{districts:[");
            text = text.Replace("]", "]}");

            Collection<District> districts = new Collection<District>();

            dynamic stuff = JObject.Parse(text);
            JArray items = (JArray)stuff["districts"];
            int length = items.Count;

            for (int i = 0; i < length; i++)
            {
                District district = new District();
                district.idDistrict = stuff.districts[i].idDistrict;
                district.Region = stuff.districts[i].Region;
                district.City = stuff.districts[i].City;
                district.Area = stuff.districts[i].Area;
                districts.Add(district);
            }
            return districts;
        }

        static Collection<Office> addOffice()
        {
            var request = WebRequest.Create("http://localhost:45227/api/Office");
            request.ContentType = "application/json; charset=utf-8";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            text = text.Replace("\"", "'");
            text = text.Replace("'я", "я");
            text = text.Replace("[", "{offices:[");
            text = text.Replace("]", "]}");

            Collection<Office> offices = new Collection<Office>();

            dynamic stuff = JObject.Parse(text);
            JArray items = (JArray)stuff["offices"];
            int length = items.Count;

            for (int i = 0; i < length; i++)
            {
                Office office = new Office();
                office.idOffice = stuff.offices[i].idOffice;
                office.Office_Title = stuff.offices[i].Office_Title;
                office.Foundation = stuff.offices[i].Foundation;
                if (stuff.offices[i].Status != null)
                    office.Status = stuff.offices[i].Status;
                office.Phone = stuff.offices[i].Phone;
                office.Address = stuff.offices[i].Address;
                offices.Add(office);
            }
            return offices;
        }

        static Collection<DeviseAction> addDeviseAction()
        {
            var request = WebRequest.Create("http://localhost:1393/api/deviseactions");
            request.ContentType = "application/json; charset=utf-8";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            text = text.Replace("\"", "'");
            text = text.Replace("'я", "я");
            text = text.Replace("[", "{deviseactions:[");
            text = text.Replace("]", "]}");

            Collection<DeviseAction> deviseactions = new Collection<DeviseAction>();

            dynamic stuff = JObject.Parse(text);
            JArray items = (JArray)stuff["deviseactions"];
            int length = items.Count;

            for (int i = 0; i < length; i++)
            {
                DeviseAction deviseaction = new DeviseAction();
                deviseaction.iddevise_actions = stuff.deviseactions[i].iddevise_actions;
                deviseaction.devise_id = stuff.deviseactions[i].devise_id;
                deviseaction.devise_date = stuff.deviseactions[i].devise_date;
                deviseaction.requester_name = stuff.deviseactions[i].requester_name;
                deviseaction.registrator_id = stuff.deviseactions[i].registrator_id;
                deviseaction.valid = stuff.deviseactions[i].valid;
                deviseaction.additional_info = stuff.deviseactions[i].additional_info;
                deviseactions.Add(deviseaction);
            }
            return deviseactions;
        }

        static Collection<Devise> addDevise()
        {
            var request = WebRequest.Create("http://localhost:1393/api/devise");
            request.ContentType = "application/json; charset=utf-8";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            text = text.Replace("\"", "'");
            text = text.Replace("'я", "я");
            text = text.Replace("[", "{devises:[");
            text = text.Replace("]", "]}");

            Collection<Devise> devises = new Collection<Devise>();

            dynamic stuff = JObject.Parse(text);
            JArray items = (JArray)stuff["devises"];
            int length = items.Count;

            for (int i = 0; i < length; i++)
            {
                Devise devise = new Devise();
                devise.devise_id = stuff.devises[i].devise_id;
                devise.alienator_name = stuff.devises[i].alienator_name;
                devise.testator_id = stuff.devises[i].testator_id;
                devise.type = stuff.devises[i].type;
                devise.storage_place = stuff.devises[i].storage_place;
                if (devise.id_notarial_acts != null && devise.id_notarial_acts > 0)
                    devise.id_notarial_acts = stuff.devises[i].id_notarial_acts;
                devise.date = stuff.devises[i].date;
                devises.Add(devise);
            }
            return devises;
        }

        static Collection<Dublicate> addDublicate()
        {
            var request = WebRequest.Create("http://localhost:1393/api/dublicate");
            request.ContentType = "application/json; charset=utf-8";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            text = text.Replace("\"", "'");
            text = text.Replace("'я", "я");
            text = text.Replace("[", "{dublicates:[");
            text = text.Replace("]", "]}");

            Collection<Dublicate> dublicates = new Collection<Dublicate>();

            dynamic stuff = JObject.Parse(text);
            JArray items = (JArray)stuff["dublicates"];
            int length = items.Count;

            for (int i = 0; i < length; i++)
            {
                Dublicate dublicate = new Dublicate();
                dublicate.iddublicates = stuff.dublicates[i].iddublicates;
                dublicate.devise_id = stuff.dublicates[i].devise_id;
                if (dublicate.id_notarial_acts != null && dublicate.id_notarial_acts > 0)
                    dublicate.id_notarial_acts = stuff.dublicates[i].id_notarial_acts;
                dublicate.date = stuff.dublicates[i].date;
                dublicate.name_issuer = stuff.dublicates[i].name_issuer;
                dublicate.place = stuff.dublicates[i].place;
                dublicates.Add(dublicate);
            }
            return dublicates;
        }

        static Collection<Testator> addTestator()
        {
            var request = WebRequest.Create("http://localhost:1393/api/testator");
            request.ContentType = "application/json; charset=utf-8";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            text = text.Replace("\"", "'");
            text = text.Replace("'я", "я");
            text = text.Replace("[", "{testators:[");
            text = text.Replace("]", "]}");

            Collection<Testator> testators = new Collection<Testator>();

            dynamic stuff = JObject.Parse(text);
            JArray items = (JArray)stuff["testators"];
            int length = items.Count;

            for (int i = 0; i < length; i++)
            {
                Testator testator = new Testator();
                testator.home = stuff.testators[i].home;
                testator.birth_place = stuff.testators[i].birth_place;
                testator.name = stuff.testators[i].name;
                testator.id_code = stuff.testators[i].id_code;
                testators.Add(testator);
            }
            return testators;
        }
        static void Main(string[] args)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();


            
            //Collection<Notary> notaries = new Collection<Notary>();
            //notaries = addNotary();
            //Collection<District> districts = new Collection<District>();
            //districts = addDistrict();
            //Collection<Office> offices = new Collection<Office>();
            //offices = addOffice();
            //Collection<DeviseAction> deviseactions = new Collection<DeviseAction>();
            //deviseactions = addDeviseAction();
            //Collection<Devise> devises = new Collection<Devise>();
            //devises = addDevise();
            //Collection<Dublicate> dublicates = new Collection<Dublicate>();
            //dublicates = addDublicate();
            //Collection<Testator> testators = new Collection<Testator>();
            //testators = addTestator();

            //foreach (Notary notary in notaries)
            //{
            //    var newNotary = notary;
            //    client.Cypher
            //    .Create("(notary:Notary {newNotary})")
            //    .WithParam("newNotary", newNotary)
            //    .ExecuteWithoutResults();
            //}
            //foreach (District district in districts)
            //{
            //    var newDistrict = district;
            //    client.Cypher
            //    .Create("(district:District {newDistrict})")
            //    .WithParam("newDistrict", newDistrict)
            //    .ExecuteWithoutResults();
            //}
            //foreach (Office office in offices)
            //{
            //    var newOffice = office;
            //    client.Cypher
            //    .Create("(office:Office {newOffice})")
            //    .WithParam("newOffice", newOffice)
            //    .ExecuteWithoutResults();
            //}
            //foreach (DeviseAction deviseaction in deviseactions)
            //{
            //    var newDeviseaction = deviseaction;
            //    client.Cypher
            //    .Create("(deviseaction:Deviseaction {newDeviseaction})")
            //    .WithParam("newDeviseaction", newDeviseaction)
            //    .ExecuteWithoutResults();
            //}
            //foreach (Devise devise in devises)
            //{
            //    var newDevise = devise;
            //    client.Cypher
            //    .Create("(devise:Devise {newDevise})")
            //    .WithParam("newDevise", newDevise)
            //    .ExecuteWithoutResults();
            //}
            //foreach (Dublicate dublicate in dublicates)
            //{
            //    var newDublicate = dublicate;
            //    client.Cypher
            //    .Create("(dublicate:Dublicate {newDublicate})")
            //    .WithParam("newDublicate", newDublicate)
            //    .ExecuteWithoutResults();
            //}
            //foreach (Testator testator in testators)
            //{
            //    var newTestator = testator;
            //    client.Cypher
            //    .Create("(estator:Testator {newTestator})")
            //    .WithParam("newTestator", newTestator)
            //    .ExecuteWithoutResults();
            //}

            //foreach (Notary notary in notaries)
            //{
            //    if (notary.District_idDistrict != null && notary.District_idDistrict > 0)
            //    {
            //        foreach (District district in districts)
            //        {
            //            if (district.idDistrict == notary.District_idDistrict)
            //            {
            //                client.Cypher
            //                .Match("(notary1:Notary)", "(district1:District)")
            //                .Where((Notary notary1) => notary1.District_idDistrict == district.idDistrict)
            //                .AndWhere((District district1) => notary.District_idDistrict == district1.idDistrict)
            //                .Create("notary1-[:DISTRICT]->district1")
            //                .ExecuteWithoutResults();
            //            }
            //        }
            //    }
            //}
            //foreach (Notary notary in notaries)
            //{
            //    if (notary.Office_idOffice != null && notary.Office_idOffice > 0)
            //    {
            //        foreach (Office office in offices)
            //        {
            //            if (office.idOffice == notary.Office_idOffice)
            //            {
            //                client.Cypher
            //                .Match("(notary1:Notary)", "(office1:Office)")
            //                .Where((Notary notary1) => notary1.Office_idOffice == office.idOffice)
            //                .AndWhere((Office office1) => notary.Office_idOffice == office1.idOffice)
            //                .Create("notary1-[:OFFICE]->office1")
            //                .ExecuteWithoutResults();
            //            }
            //        }
            //    }
            //}
            //foreach (Dublicate dublicate in dublicates)
            //{
            //    if (dublicate.devise_id != null && dublicate.devise_id > 0)
            //    {
            //        foreach (Devise devise in devises)
            //        {
            //            if (devise.devise_id == dublicate.devise_id)
            //            {
            //                client.Cypher
            //                .Match("(dublicate1:Dublicate)", "(devise1:Devise)")
            //                .Where((Dublicate dublicate1) => dublicate1.devise_id == devise.devise_id)
            //                .AndWhere((Devise devise1) => dublicate.devise_id == devise1.devise_id)
            //                .Create("dublicate1-[:DEVICE]->devise1")
            //                .ExecuteWithoutResults();
            //            }
            //        }
            //    }
            //}
            //foreach (Devise devise in devises)
            //{
            //    if (devise.testator_id != null && devise.testator_id > 0)
            //    {
            //        foreach (Testator testator in testators)
            //        {
            //            if (devise.testator_id == testator.id_code)
            //            {
            //                client.Cypher
            //                .Match("(devise1:Devise)", "(testator1:Testator)")
            //                .Where((Devise devise1) => devise1.testator_id == testator.id_code)
            //                .AndWhere((Testator testator1) => devise.testator_id == testator1.id_code)
            //                .Create("devise1-[:TESTATOR]->testator1")
            //                .ExecuteWithoutResults();
            //            }
            //        }
            //    }
            //}
            //foreach (DeviseAction deviseaction in deviseactions)
            //{
            //    if (deviseaction.devise_id != null && deviseaction.devise_id > 0)
            //    {
            //        foreach (Devise devise in devises)
            //        {
            //            if (devise.devise_id == deviseaction.devise_id)
            //            {
            //                client.Cypher
            //                .Match("(deviseaction1:Deviseaction)", "(devise1:Devise)")
            //                .Where((DeviseAction deviseaction1) => deviseaction1.devise_id == devise.devise_id)
            //                .AndWhere((Devise devise1) => deviseaction.devise_id == devise1.devise_id)
            //                .Create("deviseaction1-[:DEVISE_ACTION]->devise1")
            //                .ExecuteWithoutResults();
            //            }
            //        }
            //    }
            //}
            //foreach (DeviseAction deviseaction in deviseactions)
            //{
            //    if (deviseaction.registrator_id != null && deviseaction.registrator_id > 0)
            //    {
            //        foreach (Notary notary in notaries)
            //        {
            //            if (notary.idNotary == deviseaction.registrator_id)
            //            {
            //                client.Cypher
            //                .Match("(deviseaction1:Deviseaction)", "(notary1:Notary)")
            //                .Where((DeviseAction deviseaction1) => deviseaction1.registrator_id == notary.idNotary)
            //                .AndWhere((Notary notary1) => deviseaction.registrator_id == notary1.idNotary)
            //                .Create("deviseaction1-[:REGISTRATOR]->notary1")
            //                .ExecuteWithoutResults();
            //            }
            //        }
            //    }
            //}

            var p = client.Cypher.OptionalMatch("(user:User)-[POSTED]->(post:Post)")
                                        .Where((User user) => user.idUser == 28096902)
                                        .Return(post => post.As<Post>())
                                            .Results;
            List<Post> P = p.ToList();

            var v = client.Cypher.Match("(user:Notary)").Return(user => user.As<Notary>()).Results;

            List<Notary> N = v.ToList();
            string data = "";

            data = string.Join(";\n", N.Select(r => r.idNotary + " " + r.Surname + " "
                       + r.Name + " " + r.MiddleName + ", тел. " + r.Phone + ", ад. " + r.Address));

            Console.Write(data);          
            var stream = new MemoryStream();

            var doc = new Document();
            var writer = PdfWriter.GetInstance(doc, stream);

            doc.Open();
            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font f = new Font(bf, 12, Font.NORMAL);
            doc.Add(new Phrase(data, f));
            //Paragraph paragraph = new Paragraph(data);
            
            //doc.Add(paragraph);

            writer.CloseStream = false;
            doc.Close();

            stream.Flush();
            stream.Position = 0;

            string keyPassword = "1";
            var result = new MemoryStream(Signature.SignPdfFile(stream,
                new FileStream("Соколовська Анна.p12", FileMode.Open), keyPassword, "Generate File", "Ukraine"));
            using (FileStream file = new FileStream("result.pdf", FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[result.Length];
                result.Read(bytes, 0, (int)result.Length);
                file.Write(bytes, 0, bytes.Length);
                result.Close();
            }
        }
    }

    public class Notary
    {
        public int idNotary { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public int Certificate { get; set; }
        public DateTimeOffset Suspension { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Office_idOffice { get; set; }
        public int District_idDistrict { get; set; }
    }

    public class District
    {
        public int idDistrict { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
    }

    public class Office
    {
        public int idOffice { get; set; }
        public string Office_Title { get; set; }
        public DateTimeOffset Foundation { get; set; }
        public DateTimeOffset Status { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class DeviseAction
    {
        public int iddevise_actions { get; set; }
        public int devise_id { get; set; }
        public DateTimeOffset devise_date { get; set; }
        public string requester_name { get; set; }
        public int registrator_id { get; set; }
        public bool valid { get; set; }
        public string additional_info { get; set; }
    }

    public class Devise
    {
        public int devise_id { get; set; }
        public string alienator_name { get; set; }
        public int testator_id { get; set; }
        public string type { get; set; }
        public string storage_place { get; set; }
        public int id_notarial_acts { get; set; }
        public DateTimeOffset date { get; set; }
    }

    public class Dublicate
    {
        public int iddublicates { get; set; }
        public int devise_id { get; set; }
        public int id_notarial_acts { get; set; }
        public DateTimeOffset date { get; set; }
        public string name_issuer { get; set; }
        public string place { get; set; }
    }

    public class Testator
    {
        public string home { get; set; }
        public string birth_place { get; set; }
        public string name { get; set; }
        public int id_code { get; set; }
    }

    public class User
    {
        public int idUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public int Follovers { get; set; }
        public int Follovings { get; set; }
        public int Posts { get; set; }
    }

    public class Post
    {
        public int idUserPosted { get; set; }
        public string Photo { get; set; }
        public DateTimeOffset Data { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
    }
}
