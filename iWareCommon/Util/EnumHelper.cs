namespace iWareCommon.Util
{
    public class EnumHelper
    {
        public static int? GetEnumValue<T>(string? name)
        {

            int? v = null;

            if (string.IsNullOrEmpty(name))
            {
                return v;
            }


            foreach (var item in Enum.GetValues(typeof(T)))
            {
                if (name.Equals(item.ToString()))
                {
                    v = (int?)item;
                    break;
                }
            }
            return v;
        }


        public static T? GetEnum<T>(string? name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return default;
            }

            foreach (var item in Enum.GetValues(typeof(T)))
            {

                if (name.Equals(item.ToString()))

                {
                    return (T?)item;
                }
            }
            return default;
        }

    }
}
