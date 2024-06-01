using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto_final.Model
{
    public class DiceBearAPI
    {
        DiceBear diceBear;

        public DiceBearAPI()
        {
            diceBear = new DiceBear();
        }

        public ImageSource OnGenerateAvatar(string seed)
        {
            var avatarUrl = diceBear.Adventurer("png", seed, 100, 100);
            return ImageSource.FromUri(new Uri(avatarUrl));
        }
    }
}
