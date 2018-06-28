using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace SampleBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // Calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            //Just for fun, create a reversed instance of the message sent
            char[] reverseMessage = new char[length];
            int reverseIndex = length - 1;

            foreach (char c in activity.Text)
            {
                reverseMessage[reverseIndex] = c;
                reverseIndex--;
            }

            // Return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters. In reverse, your message is {new string(reverseMessage)}");

            context.Wait(MessageReceivedAsync);
        }
    }
}