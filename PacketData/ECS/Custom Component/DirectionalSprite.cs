using static GameData.GameDataClasses.Characters.CharacterTexture;

namespace SS13Clone.Scenes
{
    public class DirectionalSprite : Nez.Sprites.Sprite
    {
        private DirectionFace p;

       
        public DirectionalSprite(DirectionFace p)
        {
            this.p = p;
        }
    }
}