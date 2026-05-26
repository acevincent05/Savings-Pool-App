import React from 'react'
import SavingsPoolCard from '../components/SavingsPoolCard';
import { BrowserRouter, Routes, Route } from "react-router-dom";

export default function SavingsPoolList() {
  return (
    <div>
      <h1>Savings Pools!</h1>
      <SavingsPoolCard />
    </div>
  )
}
