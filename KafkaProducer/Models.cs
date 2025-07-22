namespace KafkaProducer
{
    public class SurveyResult
    {
        public int CustomerRecordId { get; set; }
        public int Rating { get; set; }
        public DateTime SurveyIssuedDate { get; set; }
        public DateTime SurveyReceivedDate { get; set; }
    }

    public class LoanSurvey
    {
        public int LoanRecordId { get; set; }
        public List<SurveyResult> SurveyResults { get; set; }
    }
}
