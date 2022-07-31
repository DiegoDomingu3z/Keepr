<template>
  <div class="p-3 m-5">
    <div class="img rounded" :style="`background-image: url(${keep.img})`">
      <h1>Hello</h1>
    </div>
  </div>
</template>


<script>
export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.img {
  width: 100%;
  height: 200px;
  background-position: center;
  background-size: cover;
  object-fit: cover;
}
</style>