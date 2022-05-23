import React, {useState, useEffect} from "react";
import { useForm } from "react-hook-form";
import "./PromotionForm.css";
import SelectTableComponent from "./TableData";
import "../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup";

export default function PromotionForm() {
  const schema = yup.object({
    Id: yup.string(),
    Type: yup.string().required().max(1),
    Description : yup.string().required().max(30),
    Value: yup.number().required()
  }).required();

  const { register, handleSubmit, formState: { errors } } = useForm({
    resolver: yupResolver(schema)
  });

  const [storeList, SetStoreList] = useState(null);
  const [clear, SetClear] = useState(false);

  useEffect(()=>{
    if(clear === true)
      SetClear(false)
  });

  const onSubmit = async (data) => {
    const formData = new FormData();
    console.log(storeList);
    var stores= storeList.map(x => x.store);
    console.log(stores)
        formData.append("Id", 'P202101010005');
        formData.append("Description", data.Description)
        formData.append("type", data.Type)
        formData.append("Value", data.Value)
        formData.append("ItemList", data.ItemList[0])
        formData.append("Stores", stores)
        formData.append("StartDate", data.startDate)
        formData.append("EndDate", data.endDate)
        const res = await fetch("http://localhost:5281/UploadPromotions", {
            method: "POST",
            body: formData,
        }).then((res) => console.log(res.json())).catch((ex)=> console.log(ex));
        console.log(res.json());
  };
  
  const onSelectedRowChange = (storeList) => {
      SetStoreList(storeList)
  }

  return (
    <>
      <form className="sub-promotion" onSubmit={handleSubmit(onSubmit)}>
        <div className="promo-item">
          Promo ID
          <input
            disabled={true}
            className="text-input"
            value="P202101010001"
            {...register("Id", { required: true })}
          />
        </div>
        <p>{errors.Id?.message}</p>
        <div className="promo-item">
          Promo Type
          <select
            onReset={0}
            className="dropdown"
            {...register("Type", { required: true })}
          >
            <option value="" selected hidden>
              Simple Discount Or Completed Discount
            </option>
            <option value="S">Simple Discount</option>
            <option value="C">Completed Discount</option>
          </select>
        </div>
        <p>{errors.Type?.message}</p>
        <div className="promo-item">
          Value Type
          <div className="value-type">
            <select onReset={0} className="dropdown-half">
              <option value="" selected hidden>
                Percentage Or Amount
              </option>
              <option value="percentage">Percentage</option>
              <option value="amount">Amount</option>
            </select>
            <input
              className="text-input reduce"
              type="number"
              maxLength={3}
              {...register("Value", { required: true })}
            />
          </div>
        </div>
        <p>{errors.Value?.message}</p>
        <div className="promo-item">
          Item
          <input type="file" {...register("ItemList", { required: true })} />
        </div>
        <p>{errors.ItemList?.message}</p>
        <div className="promo-item">
          Store
          <SelectTableComponent
            onSelectedChange={onSelectedRowChange}
          />
        </div>
        <p>{errors.Stores?.message}</p>
        <div className="promo-item">
          Promo Description
          <input
            className="text-input"
            maxLength={30}
            {...register("Description", { required: true })}
          />
        </div>
        <p>{errors.Description?.message}</p>
        <div className="promo-item">
          Promo Duration
          <div className="dates">
            <input
              type={"date"}
              className="date-input"
              {...register("startDate", { required: true })}
            />
            {" > "}
            <input
              type={"date"}
              className="date-input"
              {...register("endDate", { required: true })}
            />
          </div>
          <p>{errors.startDate?.message}</p>
          <p>{errors.endDate?.message}</p>
          <div className="operation">
            <input type="submit" className="button" />
            <input type="reset" className="button"/>
          </div>
        </div>
      </form>
    </>
  );
}
