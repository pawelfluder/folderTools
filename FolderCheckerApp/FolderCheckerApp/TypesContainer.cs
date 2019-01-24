using System;

namespace IndexTypeFinderApp
{
    public enum IndexType
    {
        unknown,
        folder,
        folder2,
        text
    }

    //usage
    //TypeContainer.GetNewObject(type);
    //Enum gg = (_enumType)Activator.CreateInstance<_enumType>();
    public class TypeContainer
    {
        public static object GetNewObject(Type t)
        {
            try
            {
                return t.GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return null;
            }
        }
    }
}
