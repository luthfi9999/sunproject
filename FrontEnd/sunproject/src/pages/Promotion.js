import React from "react";
import "./Promotion.css";
import PromotionForm from "../components/PromotionForm";
function Promotion() {
  console.log("Home");

  return (
    <div className="promotion">
      <h2 className="title">Promotion Page</h2>
      <PromotionForm />
    </div>
  );
}

export default Promotion;
