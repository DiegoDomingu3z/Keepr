import{A as o,u as r,B as n,a as c,o as p,b as u,e as d}from"./vendor.00b2d2ca.js";import{_ as i,v as l,b as m,l as _,P as v}from"./index.3da5324c.js";const f={setup(){const e=o(),s=r();return n(async()=>{try{await l.getKeeps(e.params.id),await m.getById(e.params.id)}catch(a){s.push({name:"Home"}),_.log(a),v.toast(a.message)}}),{}}},g={class:"container"};function h(e,s,a,x,B,K){const t=c("VaultKeeps");return p(),u("div",g,[d(t)])}var b=i(f,[["render",h]]);export{b as default};