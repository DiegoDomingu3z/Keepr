<template>
  <div
    class="img-container rounded selectable"
    :style="`background-image: Url(${keep.img})`"
  >
    <div class="d-flex justify-content-between mx-2 pt-1">
      <span
        ><h4>{{ keep.name }}</h4></span
      ><span @click="goToProfile"
        ><img class="profile-image" :src="keep.creator.picture" alt=""
      /></span>
    </div>
  </div>
</template>


<script>
import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    const router = useRouter()
    return {
      goToProfile() {
        router.push({ name: "Profile", params: { id: props.keep.creator.id } })
        console.log(props.keep.creator.id);
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.img-container {
  background-position: center;
  background-size: cover;
  object-fit: cover;
  height: 50vh;
}
.img-container:hover {
  transform: scale(1.02);
  transition: 350ms;
}

.profile-image {
  all: unset;
  height: 40px;
  border-radius: 50%;
}
</style>