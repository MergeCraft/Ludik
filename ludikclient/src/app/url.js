let apiUrl;

if (process.env.NODE_ENV === "development") {
  apiUrl = "https://localhost:7215";
} else {
  apiUrl = process.env.REACT_APP_API_URL;
}

export const url = apiUrl;
