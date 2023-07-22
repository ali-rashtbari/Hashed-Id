namespace Hashed_Id.Services
{
    public class IntIdHahser : IIntIdHahser
    {
        // keys ---
        public readonly IReadOnlyList<string> AlphaChars = new List<string>()
        {
            "A0Q","B0F","CG","D3Z","EX","H5W","I6V","JS","K8R","LU"
        };

        public string Code(int rawId)
        {
            try
            {
                // --- identifier --- //
                long hash_identifier = (long)Math.Pow(rawId, 2) - 1;
                string hashedId = hash_identifier.ToString() + "-";

                int rawIdLength = rawId.ToString().Length;
                long minValue = Convert.ToInt64(string.Concat("1", string.Concat(Enumerable.Repeat("0", rawIdLength - 1))));
                long maxValue = Convert.ToInt64(string.Concat(Enumerable.Repeat("9", rawIdLength)));
                long randomInt = new Random().NextInt64(minValue: minValue, maxValue: maxValue + 2);

                // --- add letters to the hashed id --- //
                char[] randomIntCharNums = randomInt.ToString().ToCharArray();
                string[] numbersArray = string.Join(",", randomIntCharNums).Split(",");
                foreach (var num in numbersArray)
                {
                    string alphabetChar = AlphaChars[Convert.ToInt32(num)];
                    hashedId += alphabetChar;
                }

                // --- modify hashed id --- //
                var hashedId_letters = hashedId.Split("-").Last();
                long hashedId_identifier = Convert.ToInt64(hashedId.Split("-").First());
                long hashedId_letters_count = hashedId_letters.Length;

                // --- regenerate the hashed id --- //
                hashedId = hashedId_identifier * hashedId_letters_count + "-" + hashedId_letters;

                return hashedId;
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }
        }

        public int Decode(string hashId)
        {
            try
            {
                // --- split and find the foundamental data about the hashed id --- //
                string[] splited_hashedId = hashId.Split("-");
                string hashedId_letters = splited_hashedId.Last();
                long hashedId_first_num = Convert.ToInt64(splited_hashedId.First());
                int hashedId_letters_count = hashedId_letters.Length;

                // --- get the identifier --- //
                long hashedId_identifier = hashedId_first_num / hashedId_letters_count;
                double rawId = Math.Sqrt((hashedId_identifier + 1));

                return Convert.ToInt32(rawId);
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }

}
