import React, { useState, useRef, useEffect } from 'react';
import axios from 'axios';
import '../ChatPanel.css';

function ChatPanel() {
  const [input, setInput] = useState('');
  const [messages, setMessages] = useState([
    { role: 'assistant', content: 'Hi! How can I help you today?' }
  ]);

  const messagesEndRef = useRef(null);

  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  }, [messages]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!input.trim()) return;

    setMessages(prev => [...prev, { role: 'user', content: input }]);

    try {
      const response = await axios.post(`${process.env.REACT_APP_API_URL}/api/chat`, {
        message: input
      });

      const assistantReply = response.data.choices[0].message.content;
//JSON.parse(response.data).choices[0].message.content;

      setMessages(prev => [...prev, { role: 'assistant', content: assistantReply }]);
    } catch (err) {
      console.error(err);
      setMessages(prev => [...prev, { role: 'assistant', content: 'Oops! Something went wrong.' }]);
    }

    setInput('');
  };

  return (
    <div className="chat-container">
      <header className="chat-header">Chat Panel</header>

      <div className="chat-messages">
        {messages.map((msg, idx) => (
          <div
            key={idx}
            className={`chat-message ${msg.role === 'user' ? 'user-message' : 'assistant-message'}`}
          >
            {msg.content}
          </div>
        ))}
        <div ref={messagesEndRef} />
      </div>

      <form onSubmit={handleSubmit} className="chat-input-form">
        <input
          type="text"
          value={input}
          onChange={e => setInput(e.target.value)}
          placeholder="Type your message..."
          className="chat-input"
        />
        <button type="submit" className="chat-send-btn">Send</button>
      </form>
    </div>
  );
}

export default ChatPanel;
