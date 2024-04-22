namespace Examination_System.ViweModels
{
    public class GetExamAnswrs
    {
        public int exam_id
        { get; set; }
        public int question_id
        { get; set; }
        public string question_text { get; set; }= null!;
        public int? selected_option { get; set; } // Nullable to handle cases where there might be no answer
        public string selected_option_text { get; set; }= null!;
    }

}
