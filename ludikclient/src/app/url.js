// Prefer build-time env var REACT_APP_API_URL, then any runtime-injected window var,
// otherwise fallback to localhost for local development.
export const url =
	process.env.REACT_APP_API_URL ||
	(typeof window !== "undefined" && window.__REACT_APP_API_URL) ||
	"https://localhost:7215";
