// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("ABZPUXRgH9IyZY8o5T6nE5E1Fr6Sjc98q0i8mW9wSnrAa0umdiyJ0UXUPcgk1wGiuBMWAYFs2JssfgG2xEdJRnbER0xExEdHRs0ZQMPErcx2xEdkdktAT2zADsCxS0dHR0NGRcfSxljGekL8ghZMlw8rE/i7nhdSZt/Kq0V4TdDzOpjucPp9UkiWHGAQ+RfDHv+EDp/vT60gYQ/6YUNuASmWrQ1r6EHI2z9NQxgO2tBZoj0qsUMlL74oe3dYbbL4IyIfhGTidsH+YW2a38eL3qy/El6Ib/px37DyqvwSAWq9ez2tWxlH6F6ZMmYROC1y26DV6zCLaV3lJlCqTJhjLTwsJCh8aP1oroRvIxoq/RU/Y7M+ORfDWxg5VHyX+dvuwURFR0ZH");
        private static int[] order = new int[] { 13,11,7,11,13,6,9,8,8,12,11,11,12,13,14 };
        private static int key = 70;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
