using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatablePlatform : MonoBehaviour {
    public const int DATABLE_PLATFORM_LAYER = 10;

    public SpriteRenderer image;
    public Sprite happySprite;
    public Sprite angrySprite;
    public string deathMessage;

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

    public DialogNode rootNode = new DialogNode {
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

    void Update() {

    }
}
