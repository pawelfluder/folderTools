using System;

namespace FolderCheckerApp
{
    public enum IndexType
    {
        unknown,
        folder,
        folder2,
        text,
        pdf,
        photos,
        index_pdf,
        reading,
        video,
        diary,
        hard,
        soft,
        voice,
        girls,
        info,
        meeting,
        flat
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
