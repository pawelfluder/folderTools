using System;

namespace FolderCheckerApp
{
    public enum IndexType
    {
        unknown,
        diary,
        folder,
        folderForPdf,
        girls,
        hard,
        info,
        meeting,
        pdf,
        photos,
        reading,
        soft,
        text,
        video,
        voice,
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
