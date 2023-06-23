import style from "./NewsRow.module.css";

export const NewsRow = (props: { title: string; content: string }) => {
  return (
    <>
      <div className={style.box}>
        {/* <h3 className={style.title}>{props.title}</h3> */}
        <h3></h3>
        {/* <p>{props.content}</p> */}
        <h3 className={style.title}>
          Binance Labs and CoinFund Lead $10 Million Funding Round for Cosmos
          Blockchain Neutron
        </h3>
        <img
          src="src/Images/neutron.jpeg"
          alt=""
          style={{ width: "300px", borderRadius: "8px" }}
        />
        <p>
          Binance Labs and CoinFund have co-led a $10 million funding round for
          Neutron, a cross-chain smart contract platform focused on interchain
          security within the Cosmos ecosystem. Binance Labs, the investment arm
          of leading cryptocurrency exchange Binance, took the lead in the
          funding round, Binance said in a Wednesday blog post. Cryptonative
          investment firm CoinFund co-led the funding. Delphi Ventures,
          LongHash, and Nomad also participated in the round. The funding will
          enable the development and growth of Neutron's blockchain software and
          promote growth for its ecosystem, Binance said in the announcement. It
          added that the goal is to create "an environment that attracts a wider
          community of developers and projects with the aim of creating
          innovative, secure, and user-friendly decentralized applications
          (DApps)."
        </p>
      </div>
      <div className={style.box}>
        {/* <h3 className={style.title}>{props.title}</h3> */}
        <h3></h3>
        {/* <p>{props.content}</p> */}
        <h3 className={style.title}>
          Binance Introduces Zero Maker Fees for TUSD Trading Pairs – The Next
          Tether Stablecoin?
        </h3>
        <img
          src="src/Images/binance.jpeg"
          alt=""
          style={{ width: "300px", borderRadius: "8px" }}
        />
        <p>
          Crypto exchange giant Binance has announced a new zero-maker fee
          promotion for stablecoin TUSD. Starting from 30th June, the
          promotional offer covers both spot and margin trading for TrueUSD. The
          zero trading fee promotion for USD stablecoin applies to all existing
          and new USD stablecoin pairs on Binance spot and margin markets, as
          per the announcement. However, the standard taker fees on these TUSD
          spot and margin trading pairs will apply as usual. Maker and taker
          fees offer a transaction rebate to traders who provide liquidity to
          the market (maker) while charging traders who get that liquidity
          (taker).
        </p>
      </div>
    </>
  );
};
