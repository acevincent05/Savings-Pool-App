import React from 'react';
import '../CSS/SavingsPoolCard.css';
import SavingsBar from './SavingsBar';
import SavingsPoolsData from "../data/SavingPools.json"

export default function SavingsPoolCard() {
  return (
    <div className="savings-pool-card-grid">
        {SavingsPoolsData.savingsPools.map(pool => (
            <a href={`/savings-pool/${pool.id}`} className="savings-pool-card-link" key={pool.id}>
                <div className="savings-pool-card">
                    <h2 className="savings-pool-card-title">{pool.title}</h2>
                    <p>Your Contributions</p>
                    <SavingsBar amount={pool.userContribution} total={pool.userTotal} />
                    <p>Overall Progress</p>
                    <SavingsBar amount={pool.overallContribution} total={pool.overallTotal} />
                </div>
            </a>
        ))}
    </div>
  )
}