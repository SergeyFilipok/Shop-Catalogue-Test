namespace Shopping {
    public class ServerResponse {
        public bool success;
        public int stusCode;
        public string message;

        public static ServerResponse FromSuccess() {
            return new ServerResponse() {
                success = true, stusCode = 200, message = "success"
            };
        }

        public static ServerResponse FromInsufficientFunds() {
            return new ServerResponse() {
                success = false, stusCode = 200, message = "Insufficient funds"
            };
        }
    }
}
