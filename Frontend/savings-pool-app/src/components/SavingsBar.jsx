import React from 'react'
import '../CSS/SavingsBar.css';

export default function SavingsBar(props) {

  return (
    <div className="savings-bar">
        <p className="savings-text">₱{props.amount} / ₱{props.total}</p>
        <div className="savings-progress" style={{ width: `${(props.amount / props.total) * 100}%` }}>
        </div>
    </div>
  )
}
