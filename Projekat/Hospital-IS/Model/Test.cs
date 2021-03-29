using System;

namespace Model
{
    public class Test
    {
        public String Diagnosis { get; set; }
        public DateTime SampleDate { get; set; }
        public String TestType { get; set; }
        public String Result { get; set; }

        public Test(string diagnosis, DateTime sampleDate, string testType, string result)
        {
            Diagnosis = diagnosis;
            SampleDate = sampleDate;
            TestType = testType;
            Result = result;
        }
    }
}