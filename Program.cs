using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(@"     _    ____  ____  _   _ _____ _____ ____");
            Console.WriteLine(@"    / \  / ___||  _ \| \ | | ____|_   _/ ___|___  _ __ ___");
            Console.WriteLine(@"   / _ \ \___ \| |_) |  \| |  _|   | || |   / _ \| '__/ _ \");
            Console.WriteLine(@"  / ___ \ ___) |  __/| |\  | |___  | || |__| (_) | | |  __/");
            Console.WriteLine(@" /_/   \_\____/|_| (_)_| \_|_____| |_(_)____\___/|_|  \___|");
            Console.WriteLine(@" __        __         _        _");
            Console.WriteLine(@" \ \      / /__  _ __| | _____| |__   ___  _ __");
            Console.WriteLine(@"  \ \ /\ / / _ \| '__| |/ / __| '_ \ / _ \| '_ \");
            Console.WriteLine(@"   \ V  V / (_) | |  |   <\__ \ | | | (_) | |_) |");
            Console.WriteLine(@"    \_/\_/ \___/|_|  |_|\_\___/_| |_|\___/| .__/");
            Console.WriteLine(@"                                          |_|");
            Console.WriteLine("___________________________________________________________");
            Console.WriteLine("\n\n");
            Console.WriteLine("Du kjører nå din første .NET Core applikasjon");
            Console.WriteLine("For å vite at du har fullført alle stegene som trengs i forkant av workshoppen,");
            Console.WriteLine("ber vi om at du registrerer deg.");
            Console.WriteLine("\n\n");
            Console.WriteLine("Hva er navnet ditt?");
            var d = Console.ReadLine();
            Console.WriteLine($"Takk, {d}!");


            HttpClient c = new HttpClient();
            Dictionary<string, Attendee> deltakere = new Dictionary<string, Attendee>();

            try
            {
                var httpContent = new StringContent("{\"navn\": \"" + d + "\"}", System.Text.Encoding.UTF8, "application/json");
                c.PostAsync(Url, httpContent).Wait();
            }
            catch (Exception)
            {
                Console.WriteLine("\n\nKunne ikke skrive data. Er du on the line?");
                return;
            }
            try
            {
                var resp = c.GetAsync(Url).Result.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(resp);
                string json = reader.ReadToEnd();
                deltakere = JsonConvert.DeserializeObject<Dictionary<string, Attendee>>(json);
            }
            catch (Exception)
            {
                Console.WriteLine("\n\nKunne ikke hente data. Er du on the line?");
                return;
            }

            Console.WriteLine("\n\nForeløpig registrerte deltakere:\n\n");

            if (deltakere.Count() == 1)
            {
                Console.WriteLine("\n\nDu er første registrerte deltaker!\n\n");
                return;
            }
            foreach (var deltaker in deltakere)
            {
                Console.WriteLine(deltaker.Value.Navn);
            }

            Console.WriteLine($"\n\nTakk for nå, {d}!\nVi sees på fagdagen!\n\n");
        }

        private const string Url = "https://aspnetcoreworkshop.firebaseio.com/users.json";

    }

    public class Attendee {
        public string Navn { get; set; }
    }
}
