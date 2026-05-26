import React from 'react'
import SavingsBar from '../components/SavingsBar';
import SavingsDetails from '../data/SavingsDetails.json';
import '../CSS/SavingsPoolDetails.css';
import { useParams } from 'react-router-dom';

export default function SavingsPoolDetails() {

  const { id } = useParams();
  const [data, setData] = React.useState(SavingsDetails.savingsDetails);

  const savingsDetail = data.find((detail) => detail.id === parseInt(id));

  return (
    <div className="savings-pool-details" key={savingsDetail.id}>
        <h1 className="savings-pool-details-title">{savingsDetail.title}</h1>
        <p>{savingsDetail.safeKeeper}</p>
        <p>{savingsDetail.contributionsFrequency}</p>
        <p>${savingsDetail.amountPerContribution.toFixed(2)}</p>
        <p>{savingsDetail.nextContributionDate}</p>
        <p>{savingsDetail.currentDate}</p>
    </div>
  )
}
