import React, { useEffect, useState } from 'react';
import '../CSS/SavingsPoolCard.css';
import SavingsBar from './SavingsBar';

export default function SavingsPoolCard({ currentUserId = 1 }) {
    const [pools, setPools] = useState([]);
    const [userContributions, setUserContributions] = useState({});
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        let cancelled = false;

        async function fetchDashboardData() {
            try {
                const poolsResponse = await fetch('/api/SavingsPools');
                const poolsData = await poolsResponse.json();

                const userResponse = await fetch(`/api/Users/${currentUserId}`);
                const userData = await userResponse.json();

                if (!cancelled) {

                    const contributionMap = {};
                    if (userData.contributions) {
                        userData.contributions.forEach(contrib => {
                            contributionMap[contrib.savingsPoolId] = contrib.amount;
                        });
                    }

                    setPools(poolsData);
                    setUserContributions(contributionMap);
                    setLoading(false);
                }
            } catch (error) {
                console.error('Error fetching paluwagan data:', error);
                if (!cancelled) setLoading(false);
            }
        }

        fetchDashboardData();

        return () => {
            cancelled = true;
        };
    }, [currentUserId]);

    if (loading) {
        return <div className="loading-status">Loading Paluwagan Dashboard...</div>;
    }

    return (
        <div className="savings-pool-card-grid">
            {pools.map(pool => {
                const poolId = pool.savingsPoolsId; 
                const yourContributionAmount = userContributions[poolId] || 0;

                return (
                    <a href={`/savings-pool/${poolId}`} className="savings-pool-card-link" key={poolId}>
                        <div className="savings-pool-card">
                            <h2 className="savings-pool-card-title">{pool.title}</h2>
                            <span className="sched-badge">{pool.schedTypeName}</span>
                            
                            <p className="progress-label">Your Contributions</p>
                            <SavingsBar amount={yourContributionAmount} total={pool.targetAmount} />
                            
                            <p className="progress-label">Overall Progress ({pool.contributorCount} members)</p>
                            <SavingsBar amount={pool.totalContributed} total={pool.targetAmount} />
                        </div>
                    </a>
                );
            })}
        </div>
    );
}