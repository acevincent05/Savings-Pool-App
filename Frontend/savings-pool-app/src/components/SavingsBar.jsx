import React from 'react';
import '../CSS/SavingsBar.css';

export default function SavingsBar({ amount = 0, total = 0 }) {

  const progressPercentage = total > 0 ? (amount / total) * 100 : 0;

  return (
    <div className="savings-bar">
      <p className="savings-text">₱{amount.toLocaleString()} / ₱{total.toLocaleString()}</p>
      <div 
        className="savings-progress" 
        style={{ width: `${Math.min(progressPercentage, 100)}%` }}
      ></div>
    </div>
  );
}