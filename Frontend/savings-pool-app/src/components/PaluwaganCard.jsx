import React from 'react';
import '../CSS/PaluwaganCard.css';
import SavingsBar from './SavingsBar';

export default function PaluwaganCard() {
  return (
    <div className="paluwagan-card-grid">
        <div className="paluwagan-card">
            <h2 className="paluwagan-card-title">Trip to Manila</h2>
            <p>Your Contributions</p>
            <SavingsBar amount={700} total={1000} />
            <p>Overall Progress</p>
            <SavingsBar amount={15000} total={50000} />
        </div>
        <div className="paluwagan-card">
            <h2 className="paluwagan-card-title">PS5 for the office</h2>
            <p>Your Contributions</p>
            <SavingsBar amount={1500} total={1500} />
            <p>Overall Progress</p>
            <SavingsBar amount={15000} total={30000} />            
        </div>

        <div className="paluwagan-card">
            <h2 className="paluwagan-card-title">New Couch</h2>
            <p>Your Contributions</p>
            <SavingsBar amount={400} total={500} />
            <p>Overall Progress</p>
            <SavingsBar amount={2000} total={9000} />
        </div>
    </div>
    
  )
}
