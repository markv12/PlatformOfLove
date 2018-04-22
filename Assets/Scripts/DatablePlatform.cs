using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatablePlatform : MonoBehaviour {
    public const int DATABLE_PLATFORM_LAYER = 10;

    public SpriteRenderer image;
    public Sprite happySprite;
    public Sprite angrySprite;
    public string deathMessage;

    public Vector3 mainTextPosition;
    public Vector3 optionsPosition;

    private bool friendly = false;
    public bool Friendly {
        get {
            return friendly;
        }
        set {
            friendly = value;
            image.sprite = friendly ? happySprite : angrySprite;
        }
    }

    public DialogNode myDialogTree {
        get {
            return dialogTrees[name];
        }
    }

    static DatablePlatform() {
        dialogTrees.Add("Platform1", p1DialogTree);
        dialogTrees.Add("Platform2", p2DialogTree);
        dialogTrees.Add("Platform3", p3DialogTree);
    }

    public static Dictionary<string,DialogNode> dialogTrees = new Dictionary<string, DialogNode>();

    private static DialogNode p1DialogTree = new DialogNode {
        text = "What do you want?",
        options = new DialogOption[] {
            new DialogOption {
                text = "N...Nothing",
                dest = new DialogNode {
                    text = "Whatever"
                }
            },
            new DialogOption {
                text = "I wanted to get a look at you.",
                dest = new DialogNode {
                    text = "What do you mean?",
                    options = new DialogOption[] {
                        new DialogOption {
                            text = "I've never seen such an ugly platform before.",
                            dest = new DialogNode {
                                text = "Well I never!",
                            }
                        },
                        new DialogOption {
                            text = "I've never seen such a beautiful platform before.",
                            dest = new DialogNode {
                                text = "You're such a charmer!",
                                options = new DialogOption[] {
                                    new DialogOption {
                                        text = "So can I step on you now?",
                                        dest = new DialogNode {
                                            text = "Certainly!",
                                            speshi = SpeshI.MakeFriendly
                                        }
                                    },
                                    new DialogOption {
                                        text = "Can I step on you in the biblical sense?",
                                        dest = new DialogNode {
                                            text = "Woah what?! no way!"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            },
            new DialogOption {
                text = "I want to step on you.",
                dest = new DialogNode {
                    text = "Gross"
                }
            }
        }
    };

    private static DialogNode p2DialogTree = new DialogNode {
        text = "Being a platform is hard, maybe I should quit.",
        options = new DialogOption[] {
            new DialogOption {
                text = "Yeah, maybe you should.",
                dest = new DialogNode {
                    text = "Sigh...",
                }
            },
            new DialogOption {
                text = "But you're a great platform!",
                dest = new DialogNode {
                    text = "There are much better platforms out there.",
                    options = new DialogOption[] {
                        new DialogOption {
                            text = "Really? Then let me through quick!",
                            dest = new DialogNode {
                                text = ":'("
                            }
                        },
                        new DialogOption {
                            text = "There must be something you're the best at.",
                            dest = new DialogNode {
                                text = "That's just wishful thinking."
                            }
                        },
                        new DialogOption() {
                            text = "Without you, the game as a whole would be worse.",
                            dest = new DialogNode {
                                text = "You're saying the game needs me?",
                                options = new DialogOption[] {
                                    new DialogOption {
                                        text = "Yes",
                                        dest = new DialogNode {
                                            text = "Really?",
                                            options = new DialogOption [] {
                                                new DialogOption {
                                                    text = "Yes",
                                                    dest = new DialogNode {
                                                        text = "Thanks, I feel a bit better",
                                                        speshi = SpeshI.MakeFriendly
                                                    }
                                                },
                                                new DialogOption {
                                                    text = "No",
                                                    dest = new DialogNode {
                                                        text = "Oh..."
                                                    }
                                                }
                                            }
                                        }
                                    },
                                     new DialogOption {
                                        text = "No",
                                        dest = new DialogNode {
                                            text = "Oh..."
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            },
            new DialogOption {
                text = "Here, let me give you a back rub.",
                dest = new DialogNode {
                    text = "What's a \"back\" also, gross.",
                }
            }
        }
    };

    private static DialogNode p3DialogTree = new DialogNode {
        text = "Can I help you?",
        options = new DialogOption[] {
            new DialogOption {
                text = "You...You too.",
                dest = new DialogNode {
                    text = "What?"
                }
            },
            new DialogOption {
                text = "Are you a girl platform?",
                dest = new DialogNode {
                    text = "I guess I've never really thought about it.",
                    options = new DialogOption[] {
                        new DialogOption {
                            text = "I've never wanted to step on a platform so badly before.",
                            dest = new DialogNode {
                                text = "I'm flattered. But are you sure it's safe?",
                                options = new DialogOption [] {
                                    new DialogOption {
                                        text = "I'll only step a little bit",
                                        dest = new DialogNode {
                                            text = "Yeah right."
                                        }
                                    },
                                    new DialogOption {
                                        text = "I'm wearing socks.",
                                        dest = new DialogNode {
                                            text = "Alright, I'm down.",
                                            speshi = SpeshI.MakeFriendly
                                        }
                                    }
                                }
                            }
                        },
                        new DialogOption {
                            text = "I don't know how I'm supposed to feel",
                            dest = new DialogNode {
                                text = "That sounds like a personal problem"
                            }
                        }
                    }
                }
            }
        }
    };
}
