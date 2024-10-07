namespace Model.Manchester
{
    internal class AUCommandWord : CommandWord
    {
        public static AUCommandWord WordUseReserve
        {
            get
            {
                return new AUCommandWord(GetWordWithData(1));
            }
        }

        public bool useReserve
        {
            get
            {
                return Data == 1;
            }
            set
            {
                if (value)
                    Data = 1;
                else
                    Data = 0;
            }
        }

        public AUCommandWord(BaseWord baseWord)
        {
            Data = baseWord.Data;
        }
    }
}
