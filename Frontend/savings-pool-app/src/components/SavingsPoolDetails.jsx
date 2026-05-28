import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import SavingsBar from '../components/SavingsBar';
import '../CSS/SavingsPoolDetails.css';

export default function SavingsPoolDetails({ currentUserId = 1 }) {
    const { id } = useParams();
    const [poolDetail, setPoolDetail] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        let isCancelled = false;

        async function fetchPoolDetails() {
            try {
                setLoading(true);
                const response = await fetch(`/api/SavingsPools/${id}`);
                
                if (!response.ok) {
                    throw new Error(`Server returned status: ${response.status}`);
                }
                
                const data = await response.ok ? await response.json() : null;

                if (!isCancelled && data) {
                    setPoolDetail(data);
                    setLoading(false);
                }
            } catch (err) {
                console.error("Error communicating with Paluwagan API:", err);
                if (!isCancelled) {
                    setError(err.message);
                    setLoading(false);
                }
            }
        }

        fetchPoolDetails();

        return () => {
            isCancelled = true;
        };
    }, [id]);

    if (loading) {
        return <div className="details-status-message">Loading Paluwagan Pool Details...</div>;
    }

    if (error || !poolDetail) {
        return (
            <div className="details-status-message error">
                <p>Error: {error || "Pool data could not be retrieved."}</p>
                <Link to="/">Return to Dashboard</Link>
            </div>
        );
    }

    const personalContribution = poolDetail.contributors?.find(c => c.userId === currentUserId)?.amount || 0;

    return (
        <div className="savings-pool-details-container">
            <div className="details-back-nav">
                <Link to="/">&larr; Back to Pools List</Link>
            </div>

            <div className="details-card-header">
                <div className="details-header-main">
                    <h1 className="savings-pool-details-title">{poolDetail.title}</h1>
                    <span className="details-sched-badge">{poolDetail.schedTypeName}</span>
                </div>
                
                <div className="details-metrics-summary">
                    <div className="metric-box">
                        <span className="metric-label">Target Goal</span>
                        <span className="metric-value">₱{poolDetail.targetAmount?.toLocaleString()}</span>
                    </div>
                    <div className="metric-box">
                        <span className="metric-label">Total Contributed</span>
                        <span className="metric-value font-success">₱{poolDetail.totalContributed?.toLocaleString()}</span>
                    </div>
                    <div className="metric-box">
                        <span className="metric-label">Total Members</span>
                        <span className="metric-value">{poolDetail.contributorCount}</span>
                    </div>
                </div>
            </div>

            <div className="details-progress-section">
                <div className="progress-group">
                    <p className="progress-group-title">Your Personal Contribution Milestone</p>
                    <SavingsBar amount={personalContribution} total={poolDetail.targetAmount} />
                </div>
                <div className="progress-group">
                    <p className="progress-group-title">Overall Pool Target Progress</p>
                    <SavingsBar amount={poolDetail.totalContributed} total={poolDetail.targetAmount} />
                </div>
            </div>

            <div className="contributors-ledger-section">
                <h2>Pool Ledger (Contributors)</h2>
                {(!poolDetail.contributors || poolDetail.contributors.length === 0) ? (
                    <p className="no-contributors">No contributors have joined this paluwagan group yet.</p>
                ) : (
                    <div className="table-responsive">
                        <table className="contributors-table">
                            <thead>
                                <tr>
                                    <th>Member Name</th>
                                    <th>Amount Contributed</th>
                                    <th>Payment Status</th>
                                    <th>Last Contribution Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {poolDetail.contributors.map((contributor) => (
                                    <tr 
                                        key={contributor.contributorId} 
                                        className={contributor.userId === currentUserId ? "highlight-user-row" : ""}
                                    >
                                        <td>
                                            {contributor.userName} 
                                            {contributor.userId === currentUserId && <span className="user-tag"> (You)</span>}
                                        </td>
                                        <td className="amount-cell">₱{contributor.amount?.toLocaleString()}</td>
                                        <td>
                                            <span className={`status-badge status-${contributor.statusId}`}>
                                                {contributor.statusName || "Active"}
                                            </span>
                                        </td>
                                        <td>
                                            {contributor.contributionDate 
                                                ? new Date(contributor.contributionDate).toLocaleDateString('en-PH', {
                                                    year: 'numeric',
                                                    month: 'long',
                                                    day: 'numeric'
                                                  })
                                                : 'No transactions recorded'}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
}