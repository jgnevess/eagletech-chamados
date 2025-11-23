import { useState } from "react";
import Markdown from "react-markdown";
import { handleChatbot } from "../../service/chatbot";
import "./index.css"

type Message = { from: "user" | "bot"; text: string };

const ChatWidget = () => {
  const [messages, setMessages] = useState<Message[]>([]);
  const [input, setInput] = useState("");
  const [open, setOpen] = useState(false);

  const sendMessage = async () => {
    if (!input.trim()) return;

    const userMessage = input;
    setMessages(prev => [...prev, { from: "user", text: userMessage }]);
    setInput("");

    const reply = await handleChatbot(input);
    setMessages(prev => [...prev, { from: "bot", text: reply }]);

  };



  return (
    <div style={{ position: "fixed", bottom: 20, right: 20, zIndex: 1050 }}>
      <button
        className="btn btn-dark rounded-circle"
        onClick={() => setOpen(!open)}
        style={{ width: 60, height: 60 }}
      >
        Chat
      </button>

      {open && (
        <div className="card" style={{ width: 300, height: 400, marginTop: 10 }}>
          <div className="card-body d-flex flex-column">
            <div
              className="flex-grow-1 overflow-auto mb-2"
              style={{ maxHeight: 320 }}
            >
              {messages.map((m, i) => (
                <div
                  key={i}
                  className={`p-2 my-1 rounded ${m.from === "user"
                    ? "bg-chat-user text-white text-end py-2"
                    : "bg-chat-bot text-start py-2"
                    }`}
                >
                  {m.from === "bot" ? <Markdown>{m.text}</Markdown> : m.text}
                </div>
              ))}
            </div>

            <div className="input-group">
              <input
                type="text"
                className="form-control"
                value={input}
                onChange={e => setInput(e.target.value)}
                onKeyDown={e => e.key === "Enter" && sendMessage()}
                placeholder="Digite sua mensagem..."
              />
              <button className="btn btn-dark" onClick={sendMessage}>
                Enviar
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}


export default ChatWidget;