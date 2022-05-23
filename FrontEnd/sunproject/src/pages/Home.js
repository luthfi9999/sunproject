import React from "react";
import { useForm } from "react-hook-form";
import "../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import "./Home.css";

export default function Home() {

  const { register, handleSubmit, formState: { errors } } = useForm();

  const onSubmit = async (data) => {
    console.log('test');
    const formData = new FormData();
        formData.append("File", data.File[0]);

        const res = await fetch("http://localhost:5281/UploadStores", {
            method: "POST",
            body: formData,
        }).then((res) => console.log(res)).catch((ex)=> console.log(ex));
        console.log(res);
  };

  return (
    <div className="home">
      <h2 className ="heading">Upload Stores</h2>
      <br />
      <form className="sub-promotion" onSubmit={handleSubmit(onSubmit)}>
        <input type="file" {...register("File", { required: true })} />
        <input type="submit" />
        <p>{errors.File?.message}</p>
      </form>
    </div>
  );
}
