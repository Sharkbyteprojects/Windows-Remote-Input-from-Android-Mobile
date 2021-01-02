using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WindowsInput.Native;
using keystoreage;
using WindowsInput;
using System.Linq;
using System.Threading.Tasks;

namespace Game
{
    public class GCService : GControl.GControlBase
    {
        private keystore mykey = new keystore();
        private readonly ILogger<GCService> _logger;
        public GCService(ILogger<GCService> logger)
        {
            _logger = logger;
        }
        private InputSimulator sim = new InputSimulator();
        public override Task<HelloReply> authorisation(auths request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Back = mykey.stringmatch(request.Authkey)
            });
        }
        public override Task<HelloReply> typeit(typesomething request, ServerCallContext context)
        {
            if (!mykey.stringmatch(request.Authkey))
            {
                return Task.FromResult(new HelloReply
                {
                    Back = false
                });
            }
            else
            {
                try
                {
                    string totype = request.Totype;
                    sim.Keyboard.TextEntry(totype);
                    Console.WriteLine(string.Format("Typed text: \"{0}\"", totype));
                    return Task.FromResult(new HelloReply
                    {
                        Back = true
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return Task.FromResult(new HelloReply
                    {
                        Back = false
                    });
                }
            }
        }
        public override Task<HelloReply> DoSomething(HelloRequest request, ServerCallContext context)
        {
            if (!mykey.stringmatch(request.Authkey))
            {
                return Task.FromResult(new HelloReply
                {
                    Back = false
                });
            }
            else
            {
                try
                {
                    char pressedkey = (char)0x00b7;
                    switch (request.Keycode)
                    {
                        case (1):
                            {//E
                                pressedkey = 'E';
                                sim.Keyboard.KeyPress(VirtualKeyCode.VK_E);
                                break;
                            }
                        case (2)://R
                            {
                                pressedkey = 'R';
                                sim.Keyboard.KeyPress(VirtualKeyCode.VK_R);
                                break;
                            }
                        case (3)://F
                            {
                                pressedkey = 'F';
                                sim.Keyboard.KeyPress(VirtualKeyCode.VK_F);
                                break;
                            }
                        case (4)://C
                            {
                                pressedkey = 'C';
                                sim.Keyboard.KeyPress(VirtualKeyCode.VK_C);
                                break;
                            }
                        case (5):
                            {
                                pressedkey = 'W';
                                sim.Keyboard.KeyPress(VirtualKeyCode.VK_W);
                                break;
                            }
                        case (6):
                            {
                                pressedkey = 'A';
                                sim.Keyboard.KeyPress(VirtualKeyCode.VK_A);
                                break;
                            }
                        case (7):
                            {
                                pressedkey = 'S';
                                sim.Keyboard.KeyPress(VirtualKeyCode.VK_S);
                                break;
                            }
                        case (8):
                            {
                                pressedkey = 'D';
                                sim.Keyboard.KeyPress(VirtualKeyCode.VK_D);
                                break;
                            }
                        case (9):
                            {
                                pressedkey = '1';
                                sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
                                break;
                            }
                        case (10):
                            {
                                pressedkey = ' ';
                                sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("NotFound");
                                break;
                            }
                    }
                    Console.WriteLine("Pressed \"{0}\"", pressedkey);
                    return Task.FromResult(new HelloReply
                    {
                        Back = true
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return Task.FromResult(new HelloReply
                    {
                        Back = false
                    });
                }
            }
        }
    }
}
