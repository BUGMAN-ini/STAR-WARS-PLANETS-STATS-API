public static class StringExtension
    {
        public static int? IsIntOrNull(this string? input)
        {
            int? result = null;
            if (int.TryParse(
                input, out int resultParsed))
            {
                result = resultParsed;
            }

            return result;
        }
    }








