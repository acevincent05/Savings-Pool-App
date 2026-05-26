import React from 'react';
import '../CSS/SavingsPoolCard.css';
import SavingsBar from './SavingsBar';
import { useEffect, useState } from 'react';

export default function SavingsPoolCard() {
    const [pools, setPools] = useState([]);

    useEffect(() => {
        let cancelled = false;
        async function fetchData() {
            try {
                const response = await fetch('/api/SavingPools');
                const data = await response.json();
                if (!cancelled) {
                    setPools(data);
                }
            } catch (error) {
                console.error('Error fetching savings pools:', error);
            }
        }

        fetchData();

        return () => {
            cancelled = true;
        };
    }, []);

  return (
    <div className="savings-pool-card-grid">
        {pools.map(pool => (
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