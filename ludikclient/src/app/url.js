export const url = process.env.PRODUCTION_API_URL || (typeof window !== "undefined" && window.PRODUCTION_API_URL) || "https://localhost:7215";
