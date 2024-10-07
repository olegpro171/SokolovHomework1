namespace Model.Manchester
{
    internal class ResponceWord : BaseWord
    {
        public ResponceWord()
        {
        }

        public ResponceWord(int Data) : base(Data)
        {
        }

        public static ResponceWord OK
        {
            get
            {
                return new ResponceWord(1);
            }
        }

        public static ResponceWord FAIL
        {
            get
            {
                return new ResponceWord(0);
            }
        }

        
    }
}
