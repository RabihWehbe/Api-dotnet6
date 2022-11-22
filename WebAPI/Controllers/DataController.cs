using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {

        private IConfiguration _config = null;

        private string folder_path = @"XYZ";

        public DataController(IConfiguration config)
        {
            Console.WriteLine("new request => new Controller object");
            _config = config;

            folder_path = _config["Root_Folder"];
        }


        [HttpGet]
        [Route("GetRabih")]
        public string test()
        {
            return "Rabih";
        }


        [HttpGet]
        [Route("GetSinglePerson")]
        public Person getPerson()
        {
            var person = new Person();
            person.Id = 0;
            person.Name = "Guest Person";
            return person;
        }


        [HttpGet]
        [Route(@"getPersonID/{id}")]
        public Person GetPersonId(int id)//where to bind?in query string or from the route
        {

            //foreach (var file in System.IO.Directory.GetDirectories(@"/App"))
            //{
            //    Console.WriteLine(file);
            //}
            //var person = new Person();
            //person.Id = id;
            //person.Name = "GuestPerson";
            //return person;

            if (System.IO.File.Exists($"{folder_path}/{id}.txt"))
            {
                string content = System.IO.File.ReadAllText($@"{folder_path}/{id}.txt");
                var person = new Person();
                string[] parts = content.Split(":");
                person.Id = Convert.ToInt32(parts[0]);
                person.Name = parts[1];
                return person;
            }
            return new Person();
        }


        [HttpGet]
        [Route("GetAllPersons")]
        public List<Person> getAllPersons()
        {
            List<Person> persons = new List<Person>();
            foreach(var file in System.IO.Directory.GetFiles(folder_path))
            {
                Console.WriteLine(file);
                string content = System.IO.File.ReadAllText(file);
                var person = new Person();
                string[] parts = content.Split(":");
                person.Id = Convert.ToInt32(parts[0]);
                person.Name = parts[1];
                persons.Add(person);
            }
            return persons;
        }

        [HttpPost]
        [Route("PostPerson")]
        public void postPerson(Person p)
        {
            Console.WriteLine("posting a new person");
            System.IO.File.WriteAllText($"{folder_path}/{p.Id}.txt", $"{p.Id}:{p.Name}");
            Console.WriteLine("Finish Posting");

        }



        [HttpDelete]
        [Route("DeletePerson/{id}")]
        public void deletePerson(int id)
        {
            Console.WriteLine(id);
            if (System.IO.File.Exists($"{folder_path}/{id}.txt"))
            {
                System.IO.File.Delete($"{folder_path}/{id}.txt");
                return;
            }
            else
            {
                Console.WriteLine("didnt find the directory!!!");
            }
            return;
        }


        [HttpPut]
        [Route("EditPerson")]
        public void editPerson(Person person){
            if (System.IO.File.Exists($"{folder_path}/{person.Id}.txt"))
            {
                Console.WriteLine("Editing a Person");
                System.IO.File.WriteAllText($"{folder_path}/{person.Id}.txt", $"{person.Id}:{person.Name}");
                return;
            }
            else
            {
                Console.WriteLine("didnt find the directory!!!");
            }
            return;
        }
    }
}
