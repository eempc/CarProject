namespace CarProject.Models {
    public class VehicleInfo {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string ImageFile {
            get {
                string fileName = Make.ToLower() + Model.ToLower() + ".png";
                return fileName;
            }
        }
    }
}
