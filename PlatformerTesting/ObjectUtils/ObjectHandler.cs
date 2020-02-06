using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTesting.ObjectUtils.BaseObjectTypes;
using System.Collections.Generic;

namespace PlatformerTesting.Objects
{
    class ObjectHandler
    {
        List<BaseObject> Objects = new List<BaseObject>();

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < Objects.Count; i++)
            { 
                Objects[i].Draw(sb);
            }
        }

        public bool CheckPoint(DynamicObject Checker, Vector2 Pos)
        {
            for (int i = 0; i < Objects.Count; i++)
            {            
                if (Objects[i] is DynamicObject)
                {
                    if (Objects[i] != Checker)
                    {
                        if(((DynamicObject)Objects[i]).GetCollider.Contains(Pos))
                            return true;
                    }
                }
            }

            return false;
        }

        public void Update(float delta)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (!Objects[i].GetDestroyed)
                    Objects[i].Update(delta);
            }

            for (int i = 0; i < Objects.Count; i++)
            {
                if (!Objects[i].GetDestroyed)
                    Objects[i].AfterUpdate(delta);
            }

            bool IsCollidingAABB(BaseObject a, BaseObject b)
            {
                return ((DynamicObject)a).GetCollider.Intersects(((DynamicObject)b).GetCollider);
            }

            for (int i = 0; i < Objects.Count; i++)
            {
                for (int j = 0; j < Objects.Count; j++)
                {
                    if (i != j)
                    {
                        if ((Objects[i] is DynamicObject) && (Objects[j] is DynamicObject))
                        {
                            if (IsCollidingAABB(Objects[i], Objects[j]))
                            {
                                ((DynamicObject)Objects[i]).OnCollide((DynamicObject)Objects[j]);
                            }
                        }
                    }
                }
            }


            for (int i = 0; i < Objects.Count; i++)
            {

                if (Objects[i].GetDestroyed)
                    Objects.RemoveAt(i);
                
            }

        }

        public void Add(BaseObject obj)
        {
            obj.SetObjectHandler(this);
            Objects.Add(obj);
        }
    }
}
