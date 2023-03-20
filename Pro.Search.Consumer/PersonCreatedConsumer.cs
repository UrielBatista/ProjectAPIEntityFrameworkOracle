using MassTransit;
using Pro.Search.PersonDomains.PersonEngine.Events;
using System.Diagnostics;

namespace Pro.Search.Consumer
{
    public class PersonCreatedConsumer : IConsumer<PersonCreatedEvent>
    {
        public Task Consume(ConsumeContext<PersonCreatedEvent> context)
        {
            var id = context.Message.Id_Pessoas;
            var email = context.Message.Email;
            var name = context.Message.Nome;

            Console.WriteLine($"Sending email to user: [{id}] - {name} | {email}");

            var psi = new ProcessStartInfo();
            psi.FileName = "C:\\PythonInstaller\\Python310\\python.exe";
            var script = "C:\\Python_Sending_Email\\Sending_Email.py";

            var message = email;
            psi.Arguments = $"\"{script}\" \"{message}\"";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process!.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            Console.WriteLine("Errors:");
            Console.WriteLine(errors);
            Console.WriteLine();
            Console.WriteLine("Results:");
            Console.WriteLine(results);

            return Task.CompletedTask;
        }
    }
}
