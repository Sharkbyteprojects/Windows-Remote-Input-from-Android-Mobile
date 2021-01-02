using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Grpc.Net.Client;
using System.Net.Http;
using System.Linq;
using Game;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace controllerclient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        string rgex(string input)
        {
            return (new Regex("[^a-zA-Z0-9 -]")).Replace(input, "");
        }
        void showerror(string error)
        {
            errorlabel.Text = string.Format("Error: {0}", error);
            errorlabel.IsVisible = true;
        }
        string address = "";
        string pinc = "";
        private async void connect(object send, EventArgs xx)
        {
            try
            {
                string inp = rgex(ConnectEntry.Text);
                string inpin = rgex(Connectpin.Text);
                if (inp != "" && inpin != "")
                {
                    var control = new GControl.GControlClient(GrpcChannel.ForAddress(inp));
                    address = inp;
                    pinc = inpin;
                    ConnectButton.IsEnabled = false;
                    HelloReply reply = await control.authorisationAsync(
                              new auths { Authkey = pinc });
                    if (reply.Back)
                    {
                        ConnectButton.IsVisible = false;
                        ConnectEntry.IsVisible = false;
                        ConnectLabel.IsVisible = false;
                        Connectpin.IsVisible = false;
                        //DO SOME AUTH THINGS
                        textinputB.IsVisible = true;
                        textinputL.IsVisible = true;
                        textinputW.IsVisible = true;
                        ek.IsVisible = true;
                        rk.IsVisible = true;
                        fk.IsVisible = true;
                        ck.IsVisible = true;
                        wk.IsVisible = true;
                        ak.IsVisible = true;
                        sk.IsVisible = true;
                        spacek.IsVisible = true;
                        dk.IsVisible = true;
                        onek.IsVisible = true;
                    }
                    else
                    {
                        showerror("Wrong Pin");
                    }
                    ConnectButton.IsEnabled = true;
                }
                else
                {
                    showerror("Enter valid things!");
                }
            }
            catch (Exception e)
            {
                showerror(e.Message);
            }
        }
        private async Task sendkeystroke(int stroke)
        {
            try
            {
                if (address != "" && pinc != "")
                {
                    var channel = GrpcChannel.ForAddress(address);
                    var control = new GControl.GControlClient(channel);
                    HelloReply reply = await control.DoSomethingAsync(
                              new HelloRequest { Keycode = stroke, Authkey = pinc });
                    if (!reply.Back)
                    {
                        showerror(string.Format("Can't execute stroke {0}, look at server for logs!", stroke));
                    }
                }
                else
                {
                    showerror("This Should not happend! You call without init");
                }
            }
            catch (Exception e)
            {
                showerror(e.Message);
            }
        }
        private async void writeout(object send, EventArgs xx) {
            try
            {
                string stroke = textinputW.Text;
                if (address != "" && pinc != "")
                {
                    var channel = GrpcChannel.ForAddress(address);
                    var control = new GControl.GControlClient(channel);
                    HelloReply reply = await control.typeitAsync(
                              new typesomething { Totype = stroke, Authkey = pinc });
                    if (!reply.Back)
                    {
                        showerror(string.Format("Can't execute type \"{0}\", look at server for logs!", stroke));
                    }
                    else
                    {
                        textinputW.Text = "";
                    }
                }
                else
                {
                    showerror("This Should not happend! You call without init");
                }
            }
            catch (Exception e)
            {
                showerror(e.Message);
            }
        }
        private async void e(object send, EventArgs xx) { await sendkeystroke(1); }
        private async void r(object send, EventArgs xx) { await sendkeystroke(2); }
        private async void f(object send, EventArgs xx) { await sendkeystroke(3); }
        private async void c(object send, EventArgs xx) { await sendkeystroke(4); }
        private async void w(object send, EventArgs xx) { await sendkeystroke(5); }
        private async void a(object send, EventArgs xx) { await sendkeystroke(6); }
        private async void s(object send, EventArgs xx) { await sendkeystroke(7); }
        private async void d(object send, EventArgs xx) { await sendkeystroke(8); }
        private async void one(object send, EventArgs xx) { await sendkeystroke(9); }
        private async void space(object send, EventArgs xx) { await sendkeystroke(10); }
    }
}
