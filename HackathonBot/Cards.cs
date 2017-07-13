using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;

namespace HackathonBot
{
    public static class Cards
    {
        public static Attachment GetHeroCard(string imageUrl, string link, string title, string subtitle)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Images = new List<CardImage> { new CardImage(imageUrl) },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "View", value: link) }
            };

            return heroCard.ToAttachment();
        }

        public static Attachment GetInternetAttachment(string url, string filename = "image.jpg")
        {
            return new Attachment
            {
                Name = filename,
                ContentType = "image/png",
                ContentUrl = url
            };
        }

        public static Attachment GetAnimationCard(string imageUrl)
        {
            var animationCard = new AnimationCard
            {
                Title = "Microsoft Bot Framework",
                Subtitle = "Animation Card",
                Image = new ThumbnailUrl
                {
                    Url = imageUrl
                },
                Media = new List<MediaUrl>
        {
            new MediaUrl()
            {
                Url = imageUrl
            }
        }
            };

            return animationCard.ToAttachment();
        }
    }
}