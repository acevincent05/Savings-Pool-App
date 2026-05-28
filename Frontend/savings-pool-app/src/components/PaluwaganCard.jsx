import React, { useEffect, useState } from 'react';
import '../CSS/SavingsPoolCard.css';
import SavingsBar from './SavingsBar';

export default function SavingsPoolCard({ currentUserId = 1 }) {
    const [pools, setPools] = useState([]);
    const [userContributions, setUserContributions] = useState({});
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    const normalizePool = (pool) => ({
        id: pool.savingsPoolsId ?? pool.SavingsPoolsId ?? pool.id,
        title: pool.title ?? pool.Title ?? 'Untitled pool',
        targetAmount: Number(pool.targetAmount ?? pool.TargetAmount ?? 0),
        totalContributed: Number(pool.totalContributed ?? pool.TotalContributed ?? 0),
        contributorCount: Number(pool.contributorCount ?? pool.ContributorCount ?? 0),
        schedTypeName: pool.schedTypeName ?? pool.SchedTypeName ?? '',
    });

    useEffect(() => {
        let cancelled = false;

        async function fetchDashboardData() {
            try {
                const poolsResponse = await fetch('/api/SavingsPools');
                if (!poolsResponse.ok) {
                    throw new Error(`Savings pools request failed (${poolsResponse.status})`);
                }
                const poolsData = await poolsResponse.json();

                const userResponse = await fetch(`/api/Users/${currentUserId}`);
                if (!userResponse.ok) {
                    throw new Error(`User request failed (${userResponse.status})`);
                }
                const userData = await userResponse.json();

                if (!cancelled) {

                    const contributionMap = {};
                    const contributions = userData.Contributions ?? userData.contributions ?? [];
                    contributions.forEach(contrib => {
                        const savingsPoolId = contrib.SavingsPoolId ?? contrib.savingsPoolId ?? contrib.savingsPoolID;
                        contributionMap[savingsPoolId] = contrib.Amount ?? contrib.amount ?? 0;
                        });

                    setPools(poolsData.map(normalizePool));
                    setUserContributions(contributionMap);
                    setError('');
                    setLoading(false);
                }
            } catch (error) {
                console.error('Error fetching paluwagan data:', error);
                if (!cancelled) {
                    setError('Unable to load paluwagan data right now. Check that the backend is running.');
                    setLoading(false);
                }
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

    if (error) {
        return <div className="loading-status">{error}</div>;
    }

    return (
        <div className="savings-pool-card-grid">
            {pools.map(pool => {
                const poolId = pool.id;
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