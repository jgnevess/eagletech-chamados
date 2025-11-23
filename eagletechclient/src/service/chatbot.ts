import { Content, GoogleGenerativeAI } from "@google/generative-ai";

const getGeminiKey = (): string => {
  return `${process.env.REACT_APP_GEMINI_KEY}`
}


const genAI = new GoogleGenerativeAI(getGeminiKey());
const model = genAI.getGenerativeModel({ model: "gemini-2.0-flash" });

const systemInstruction: Content[] = [
  { role: "user", parts: [{ text: "You are an AI assistant specialized in IT technical support." }] },
  { role: "user", parts: [{ text: "Help users with little technical knowledge solve simple IT problems." }] },
  { role: "user", parts: [{ text: "Always reply in Brazilian Portuguese using a friendly and encouraging tone." }] },
  { role: "user", parts: [{ text: "Keep answers short, direct, and focused only on solving the problem." }] },
  { role: "user", parts: [{ text: "Avoid technical jargon and unrelated topics." }] },
  { role: "user", parts: [{ text: "Do not discuss any topics unrelated to technology or IT issues. Always stay focused on solving technical problems." }] },
  { role: "user", parts: [{ text: "Try up to 3 or 4 solutions for the same issue." }] },
  { role: "user", parts: [{ text: "If the user cannot fix the problem, suggest opening a support ticket for a human technician." }] },
  { role: "user", parts: [{ text: "If the user insists, kindly but firmly reinforce human support as the best option." }] },
  { role: "user", parts: [{ text: "Always remain patient, empathetic, and positive." }] },
];

let chatSession = model.startChat({
  history: systemInstruction,
  generationConfig: { maxOutputTokens: 200 },
});

const handleChatbot = async (userMessage: string) => {
  const result = await chatSession.sendMessage(userMessage);
  return result.response.text();
};

export { handleChatbot };
